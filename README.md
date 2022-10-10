# ASP.NET Dotnet framework App > published into Container > deployed to AKS Windows node - connecting Azure Redis SSL endpoint 

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat 

```docker 
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2019
ARG source
WORKDIR /inetpub/wwwroot
COPY ${source:-obj/Docker/publish} .

```

```bash 
docker 
docker login mikkyshoprepo.azurecr.io -u mikkyshoprepo -p something
docker tag mikkyredis2 mikkyshoprepo.azurecr.io/mikkyredis2:latest
docker push mikkyshoprepo.azurecr.io/mikkyredis2:latest
```

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

<img width="463" alt="1" src="https://user-images.githubusercontent.com/61469290/194805844-5cf94de5-7ca6-4f44-ba01-9c0f4de60c9f.png">

<img width="738" alt="2" src="https://user-images.githubusercontent.com/61469290/194805850-b2d17b0a-ac3b-4f6d-b2b4-3e2534ce31fd.png">
<img width="733" alt="3" src="https://user-images.githubusercontent.com/61469290/194805857-4eb74e18-2b15-4c64-a6ef-f48581ebbc1f.png">
<img width="657" alt="4" src="https://user-images.githubusercontent.com/61469290/194805866-f394a8dd-2420-4f3e-b6e0-19d99664146e.png">
