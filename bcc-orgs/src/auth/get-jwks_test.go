package auth

import (
	"testing"
)

func TestGetJWKS(t *testing.T) {
	GetKeySet(auth0Issuer)
}
