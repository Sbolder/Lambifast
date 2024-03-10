
# Build Solution

```pwsh
sam build
```

# Deploy

```pwsh
sam deploy --stack-name lambda-sample `
    --capabilities CAPABILITY_IAM CAPABILITY_NAMED_IAM `
    --region eu-central-1 `
    --resolve-s3 `
    --no-fail-on-empty-changeset `
    --parameter-overrides Environment="int" `
    PrivateSubnets="subnet-057aef1d0fc1573be,subnet-04ebe072e7a77858b" `
    SecurityGroup="sg-09f7b65b52cb2c0c5" `
    --profile asf
```

# UnDeploy

```pwsh
sam delete --stack-name lambda-sample `
    --profile asf
```