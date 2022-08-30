# global-dashboard

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Run your unit tests
```
npm run test:unit
```

### Run your end-to-end tests
```
npm run test:e2e
```

### Lints and fixes files
```
npm run lint
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).


# Settings
## Local
Create a file .env.local
- {_these items are found in AWS_} 
  - When you paste, replace everything including the {}
- {these items are found in CODE} 
  - The code will replace everything including the {}

VUE_APP_HIVE_END_POINT_API=https://localhost:58896/hive/api
VUE_APP_IMP_AUTHORIZATION_END_POINT=https://{_Cognito Domain of your App Integration_}/oauth2/authorize?response_type=code&client_id={_App Integration Client Id_}&redirect_uri=http://localhost:8080/vue-js/&state={NONCE}
VUE_APP_TOKEN_END_POINT=https://{_Cognito Domain of your App Integration_}/oauth2/token
VUE_APP_REDIRECT_URI=http://localhost:8080/vue-js/
VUE_APP_CLIENT_ID={_App Integration Client Id_}
VUE_APP_CLIENT_SECRET={_App Integration Client Secret_}