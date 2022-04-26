package auth0

import (
	"context"
	"fmt"

	"github.com/lestrrat-go/jwx/jwk"
)

var keySet jwk.Set = nil

// GetKeySet for the specified domain.
// The result is cached for the lifetime of the program
func GetKeySet(domain string) jwk.Set {
	if keySet != nil {
		return keySet
	}

	keyURL := fmt.Sprintf("%s/.well-known/jwks.json", domain)
	fechedKeySet, err := jwk.Fetch(context.Background(), keyURL)
	if err != nil {
		panic(err)
	}
	keySet = fechedKeySet
	return keySet
}
