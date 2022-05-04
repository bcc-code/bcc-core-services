FROM node:14.15.5-buster as build
WORKDIR /app

COPY package.json package-lock.json ./
COPY src ./src
COPY public ./public
RUN npm install

CMD ["npm", "start"]
