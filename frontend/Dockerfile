FROM node:16.13.1-alpine

WORKDIR /usr/src/app/api

COPY package*.json ./

RUN npm install

COPY . .

EXPOSE 3000

CMD ["npm", "run", "dev"]