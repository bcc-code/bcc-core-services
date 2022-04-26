package utils

import (
	"os"
	"path/filepath"
	"runtime"

	"github.com/joho/godotenv"
)

func InitEnv() error {
	var err error

	// Env file is allready added by Docker Compose
	if os.Getenv("ENVIRONMENT") != "localhost" {
		_, b, _, _ := runtime.Caller(0)
		projectRootPath := filepath.Join(filepath.Dir(b), "../../")
		path := string(projectRootPath) + `/.env`

		err = godotenv.Load(path)
	}

	return err

}
