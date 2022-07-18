FROM node:current-slim as build

# install dependencies
WORKDIR /app
COPY package.json ./
RUN npm i

# Copy all local files into the image.
COPY . .

RUN npm run build

###
# Only copy over the Node pieces we need
# ~> Saves 35MB
###
FROM node:current-slim

WORKDIR /app
COPY --from=build /app/.svelte-kit/build/. .

EXPOSE 3000
CMD ["node", "./app.js"]