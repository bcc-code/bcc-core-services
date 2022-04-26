package services

import (
	"bcc-orgs/src/utils"
	"fmt"
	"os"
	"sort"
	"testing"
)

func TestMain(m *testing.M) {

	dbErr := utils.OpenDb()
	if dbErr != nil {
		panic(dbErr)
	} else {
		fmt.Println("DB connected")
	}
	exitVal := m.Run()
	os.Exit(exitVal)
}

func TestCreateOrg(t *testing.T) {
	orgs, err := FindOrgs()

	if err != nil {
		panic(err)
	}
	sort.Slice(orgs, func(i, j int) bool {
		return orgs[i].OrgID < orgs[j].OrgID
	})

	fmt.Println("Sorting test", orgs[0])

	// createdOrg, err := CreateOrg(models.Org{})

	// if createdOrg
	// t.Fatal()
}
