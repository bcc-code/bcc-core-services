# Github Actions Docs

Info about Github Actions CI/CD process

# Add Comments in PR durring CI/CD process

For comments we are using package:
https://github.com/unsplash/comment-on-pr

1. Ensure that "gcloud depoly" command return proper output and save it to the file:

```
 --user-output-enabled --format=json | tee -a gcloud_output.json
```

2. Add Github Acions step which convert json properties into github properties. Pipe | tr -d \" removes quote signs from string:

```
      - name: Get result from JSON file
        id: gcloud_result
        run: |
          echo "::set-output name=URL::$(cat gcloud_output.json | jq '.status.address.url' | tr -d \")"
          echo "::set-output name=REVISION_NAME::$(cat gcloud_output.json | jq '.status.traffic[0].revisionName' | tr -d \")"
```

3. Add step with github comment and add variables from previous steps

```
      - name: Comment on PR
        uses: unsplash/comment-on-pr@v1.3.0
        env:
          GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
        with:
          msg: "DEV Api url: ${{ steps.gcloud_result.outputs.URL }} \n\n
            DEV Revision name: ${{ steps.gcloud_result.outputs.REVISION_NAME }} "
          check_for_duplicate_msg: false
```
