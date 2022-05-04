FROM node:14.15.5-buster as build
WORKDIR /app

COPY package.json package-lock.json ./
COPY src ./src
COPY specs ./specs
RUN npm install

CMD ["npm", "start"]
