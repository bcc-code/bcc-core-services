package utils

import (
	"github.com/joho/godotenv"
)

func InitEnv() {
	_ = godotenv.Load()
}
