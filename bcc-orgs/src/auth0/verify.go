package auth0

import (
	"bcc-orgs/src/utilities"
	"net/http"
	"os"
	"strings"

	"github.com/gin-gonic/gin"
	"github.com/lestrrat-go/jwx/jwt"
)

var (
	auth0Issuer   = os.Getenv("AUTH0_ISSUER")
	auth0Audience = os.Getenv("AUTH0_AUDIENCE")
)

func JWTCheckWithScopes(scopes []string) gin.HandlerFunc {
	return func(c *gin.Context) {
		token, err := parseToken(c)
		if err != nil {
			c.AbortWithStatusJSON(http.StatusUnauthorized, "Couldn't parse token")
			return
		}

		err = jwt.Validate(
			token,
			jwt.WithAudience(auth0Audience),
		)
		if err != nil {
			c.AbortWithStatusJSON(http.StatusUnauthorized, "Invalid audience")
			return
		}

		if !checkScopes(token, scopes) {
			c.AbortWithStatusJSON(http.StatusUnauthorized, "Missing scopes")
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
		if !utilities.Contains(tokenScopes, requiredScope) {
			return false
		}
	}
	return true
}
