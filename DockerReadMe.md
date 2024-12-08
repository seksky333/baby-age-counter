###Build docker image###
docker build -t s---3/baby-age-counter:{image-version} -f Dockerfile .
###Push docker image ###
docker image push s---3/baby-age-counter:{image-version}
