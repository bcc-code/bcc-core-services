package auth

import (
	"context"
	"fmt"

	"github.com/lestrrat-go/jwx/jwk"
)

var keySet jwk.Set = nil

func GetKeySet(domain string) jwk.Set {
	if keySet != nil {
		return keySet
	}

	keyURL := fmt.Sprintf("https://%s/.well-known/jwks.json", domain)
	fechedKeySet, err := jwk.Fetch(context.Background(), keyURL)
	if err != nil {
		fmt.Printf("Failed to fetch key set: %v", err)
		panic(err)
	}
	keySet = fechedKeySet
	return keySet
}
