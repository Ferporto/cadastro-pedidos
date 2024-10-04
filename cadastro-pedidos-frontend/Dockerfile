# Use a imagem Node.js para compilar o Angular
FROM node:20 AS build
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build

# Use Nginx para servir a aplicação Angular
FROM nginx:alpine
COPY --from=build /app/dist/cadastro-pedidos/browser /usr/share/nginx/html
EXPOSE 80
