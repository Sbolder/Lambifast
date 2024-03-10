echo "START BUILD"
sam build
echo "END BUILD"
echo "-------------"
echo "START RELEASE"
$stackName = "lambifast-demo"
$capabilities = "CAPABILITY_IAM", "CAPABILITY_NAMED_IAM"
$region = "eu-central-1"
$s3Bucket = "lambifast-sample"
$environment = "demo"

# Costruisci il comando sam deploy
$deployCommand = "sam deploy --stack-name $stackName --capabilities $capabilities --region $region --s3-bucket $s3Bucket --no-fail-on-empty-changeset --parameter-overrides Environment='$environment'"

# Esegui il comando
Invoke-Expression -Command $deployCommand





