package auth

import (
	"bytes"
	"encoding/json"
	"fmt"
	"io/ioutil"
	"net/http"
	"os"

	"github.com/gin-gonic/gin"
)

var (
	auth0ClientID     = os.Getenv("AUTH0_CLIENT_ID")
	auth0ClientSecret = os.Getenv("AUTH0_CLIENT_SECRET")
)

type TokenResponse struct {
	AccessToken string  `json:"access_token"`
	ExpiresIn   int     `json:"expires_in"`
	TokenType   string  `json:"token_type"`
	Scope       *string `json:"scope"`
}

func GetToken(audience string, scope string) (string, error) {
	tokenUrl := fmt.Sprintf("https://%s/oauth/token", auth0Issuer)
	postBody, err := json.Marshal(gin.H{
		"grant_type":    "client_credentials",
		"client_id":     auth0ClientID,
		"client_secret": auth0ClientSecret,
		"audience":      audience,
		"scope":         scope,
	})
	if err != nil {
		return "", err
	}
	requestBody := bytes.NewBuffer(postBody)
	resp, err := http.Post(tokenUrl, "application/json", requestBody)
	if err != nil {
		return "", err
	}
	body, err := ioutil.ReadAll(resp.Body)
	if err != nil {
		return "", err
	}
	var tokenResponse TokenResponse
	err = json.Unmarshal(body, &tokenResponse)
	return tokenResponse.AccessToken, nil
}
