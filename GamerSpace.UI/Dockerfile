FROM node:20-alpine AS build
WORKDIR /app

COPY package*.json ./
RUN npm install --ignore-scripts

COPY angular.json tsconfig*.json ./

COPY src/ ./src/

COPY public/ ./public/

RUN npm run build -- --configuration=production

FROM nginxinc/nginx-unprivileged:alpine AS runtime

COPY nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist/GamerSpace.UI/browser /usr/share/nginx/html

EXPOSE 8080

CMD ["nginx", "-g", "daemon off;"]