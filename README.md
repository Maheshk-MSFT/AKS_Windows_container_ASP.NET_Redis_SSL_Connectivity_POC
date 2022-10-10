# mikkyredis

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
