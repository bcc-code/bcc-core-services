package auth

import (
	"net/http"
	"os"
	"strings"

	"github.com/gin-gonic/gin"
	"github.com/lestrrat-go/jwx/jwt"
	"github.com/samber/lo"
)

var (
	auth0Issuer   = os.Getenv("AUTH0_ISSUER")
	auth0Audience = os.Getenv("AUTH0_AUDIENCE")
)

func JWTCheckWithScopes(scopes []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		token, err := parseToken(c)
		if err != nil {
			c.String(http.StatusUnauthorized, "Couldn't parse token")
			c.Abort()
			return
		}

		err = jwt.Validate(
			token,
			jwt.WithAudience(auth0Audience),
		)
		if err != nil {
			c.String(http.StatusUnauthorized, "Invalid audience")
			c.Abort()
			return
		}

		if !checkScopes(token, scopes) {
			c.String(http.StatusUnauthorized, "Missing scopes")
			c.Abort()
			return
		}
	}
}

func parseToken(c *gin.Context) (jwt.Token, error) {
	token, err := jwt.ParseRequest(
		c.Request,
		jwt.WithKeySet(GetKeySet(auth0Issuer)),
		jwt.WithHeaderKey("Authorization"),
	)
	return token, err
}

func checkScopes(token jwt.Token, requiredScopes []string) bool {
	scope, ok := token.Get("scope")
	if !ok {
		return false
	}
	tokenScopes := strings.Split(scope.(string), " ")
	for _, requiredScope := range requiredScopes {
		if !lo.Contains(tokenScopes, requiredScope) {
			return false
		}
	}
	return true
}
