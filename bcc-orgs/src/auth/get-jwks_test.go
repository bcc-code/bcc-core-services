package auth

import (
	"fmt"
	"testing"
)

func TestGetJWKS(t *testing.T) {
	keySet := GetKeySet(auth0Issuer)
	fmt.Printf("%+v\n", keySet)
}
