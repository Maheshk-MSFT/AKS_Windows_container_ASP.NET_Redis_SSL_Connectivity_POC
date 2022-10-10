# mikkyredis
#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat 

```docker 
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2019
ARG source
WORKDIR /inetpub/wwwroot
COPY ${source:-obj/Docker/publish} .

```

docker 
docker login mikkyshoprepo.azurecr.io -u mikkyshoprepo -p something
docker tag mikkyredis2 mikkyshoprepo.azurecr.io/mikkyredis2:latest
docker push mikkyshoprepo.azurecr.io/mikkyredis2:latest

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: sampleredis
  labels:
    app: sampleredis
spec:
  replicas: 1
  template:
    metadata:
      name: sampleredis
      labels:
        app: sampleredis
    spec:
      nodeSelector:
        "kubernetes.io/os": windows
      containers:
      - name: sampleredis
        image: mikkyshoprepo.azurecr.io/mikkyredis2:latest
        resources:
          limits:
            cpu: 1
            memory: 800M
        ports:
          - containerPort: 80
  selector:
    matchLabels:
      app: sampleredis
---
apiVersion: v1
kind: Service
metadata:
  name: sampleredis
spec:
  type: LoadBalancer
  ports:
  - protocol: TCP
    port: 80
  selector:
    app: sampleredis
```
