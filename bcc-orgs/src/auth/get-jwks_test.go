package auth

import (
	"fmt"
	"testing"
)

func TestGetJWKS(t *testing.T) {
	keySet := GetKeySet(auth0Issuer)
	panic("Whatever")
	fmt.Printf("%+v\n", keySet)
}
