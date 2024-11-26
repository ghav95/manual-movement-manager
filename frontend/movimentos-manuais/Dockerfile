# Stage 1: Build the Angular app
FROM node:18.19.0 as build

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

RUN npx ng build --configuration=production

# Stage 2: Serve the app with NGINX
FROM nginx:stable-alpine as production

# Copy the built Angular app to NGINX's static file directory
COPY --from=build /app/dist/movimentos-manuais /usr/share/nginx/html

# Copy the custom NGINX configuration
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Expose port 4200
EXPOSE 4200

# Start NGINX
CMD ["nginx", "-g", "daemon off;"]
