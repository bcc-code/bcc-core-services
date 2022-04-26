package utilities

func Contains[T string | int](slice []T, item T) bool {
	for _, elm := range slice {
		if elm == item {
			return true
		}
	}
	return false
}
