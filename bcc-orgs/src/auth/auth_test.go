package auth

import (
	"net/http"
	"net/http/httptest"
	"testing"

	"github.com/gin-gonic/gin"
	"github.com/stretchr/testify/assert"
)

func TestGetJWKS(t *testing.T) {
	GetKeySet(auth0Issuer)
}

func TestGetToken(t *testing.T) {
	token, err := GetToken(auth0Audience, "")
	assert.NoError(t, err)
	assert.NotEmpty(t, token)
}

func TestJWTCheckNoToken(t *testing.T) {
	r := getEndpointWithScopeCheck([]string{})
	w := PerformRequest(r, "GET", "/")
	assert.Equal(t, http.StatusUnauthorized, w.Code)
	assert.Equal(t, w.Body.String(), "Couldn't parse token")
}

func TestJWTCheckInvalidAudience(t *testing.T) {
	token, _ := GetToken("test", "")
	r := getEndpointWithScopeCheck([]string{})
	w := PerformRequest(r, "GET", "/", map[string]string{"Authorization": "Bearer " + token})
	assert.Equal(t, http.StatusUnauthorized, w.Code)
	assert.Equal(t, "Invalid audience", w.Body.String())
}

func TestJWTCheckMissingScope(t *testing.T) {
	token, _ := GetToken(auth0Audience, "org#write")
	r := getEndpointWithScopeCheck([]string{"org#read"})
	w := PerformRequest(r, "GET", "/", map[string]string{"Authorization": "Bearer " + token})
	assert.Equal(t, http.StatusUnauthorized, w.Code)
	assert.Equal(t, "Missing scopes", w.Body.String())
}

func TestJWTCheckValidToken(t *testing.T) {
	token, _ := GetToken(auth0Audience, "org#read")
	r := getEndpointWithScopeCheck([]string{"org#read"})
	w := PerformRequest(r, "GET", "/", map[string]string{"Authorization": "Bearer " + token})
	assert.Equal(t, http.StatusOK, w.Code)
}

func getEndpointWithScopeCheck(scopes []string) *gin.Engine {
	r := gin.Default()
	r.GET("/", JWTCheckWithScopes(scopes), func(c *gin.Context) {
		c.Status(http.StatusOK)
	})
	return r
}

func PerformRequest(r http.Handler, method, path string, headers ...map[string]string) *httptest.ResponseRecorder {
	req := httptest.NewRequest(method, path, nil)
	if len(headers) > 0 {
		for k, v := range headers[0] {
			req.Header.Set(k, v)
		}
	}
	w := httptest.NewRecorder()
	r.ServeHTTP(w, req)
	return w
}
