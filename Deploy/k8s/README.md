# Kubernetes Deployment (PostgreSQL + mPortal)

This directory contains baseline Kubernetes manifests for running mPortal with PostgreSQL.

## Apply Order

1. `namespace.yaml`
2. `postgres-secret.example.yaml` (copy to `postgres-secret.yaml` and replace placeholders)
3. `postgres-statefulset.yaml`
4. `web-configmap.yaml`
5. `web-deployment.yaml`
6. `web-service.yaml`

## Notes

- `postgres-secret.example.yaml` is a template only and should not be used as-is.
- Use your cluster ingress/service policy for external exposure.
- This is a baseline deployment profile for migration validation; hardening (HA, backups, TLS, network policies) should be added per environment.
