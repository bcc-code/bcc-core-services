---
title: Core Services
description: Core/shared APIs
---


# Working with GitHub

## Branching
We use a scaled trunk-based development for short living branches (1 day at most).

Read more: https://trunkbaseddevelopment.com/#scaled-trunk-based-development

**Important info - all commits pushed to the master branch will trigger a release to production. Be aware while merging PRs.**

## Creating a pull request

1. Create a pull request with a description [Keyword] + [Issue number] (`Closes #600` or `Fixes #600`) for bcc-myshare repository. <br>Use `Closes bcc-code/bcc-collections#40` to link to an issue in another repo, bcc-collection repository in this case.
2. Ensure that a project is select for the pull request
3. (Important) Select `Incident` label for bugs that affect end-users and are introduced in the production (master branch)*

Read more: https://docs.github.com/en/issues/tracking-your-work-with-issues/linking-a-pull-request-to-an-issue

*We use pull requests to measure our KPIs. Read more in `KPIs` section.

## Code-review - checklist

[Read more](.docs/Processes/CodeReview.md) in the documentation

# KPIs
We use [Haystack](https://dash.usehaystack.io/app/overview) to measure our speed and quality. It is based on the pull requests so it is important to build-in that in our routines and culture. **Only the pull requests count**, everything else is working automatically/out-of-box.

Four key metrics:
1. Lead time
2. Deployment frequency
3. Time to restore
4. Change failure rate

Links: 
- [Accelerate book](https://www.amazon.com/Accelerate-Software-Performing-Technology-Organizations/dp/1942788339)
- [DORA](https://cloud.google.com/architecture/devops/technical)
- [Medium](https://medium.com/ingeniouslysimple/learning-from-the-accelerate-four-key-metrics-91725675e30a)


# Release processes

Important thing to mention - all commits pushed to the master will result in a release to production. Changes pushed to the master need to be done ONLY via pull requests.

See [Releasing a new feature](release-new-feature.md)
