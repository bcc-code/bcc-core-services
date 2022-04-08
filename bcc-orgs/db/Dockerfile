FROM postgres:13.6-alpine
COPY migrations /docker-entrypoint-initdb.d/

ENV POSTGRES_USER admin
ENV POSTGRES_PASSWORD password1234
ENV POSTGRES_DB orgsdb