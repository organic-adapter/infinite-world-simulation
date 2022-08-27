import axios from 'axios';
declare global {
    interface Crypto {
      randomUUID: () => string;
    }
  }


export class Services {
    async checkAuth() {
        const token = localStorage.getItem("bearer-token");
        if(token === null) {
            const nonce = crypto.randomUUID();
            const login = process.env?.VUE_APP_IMP_AUTHORIZATION_END_POINT.replace("{NONCE}", nonce);
            window.open(login, '_self');
        }
    }
    getHiveEndpoints(): Array<string> {
        let endpoints: Array<string> = [];
        axios.get(`${process.env.VUE_APP_HIVE_END_POINT_API}/endpoint`)
            .then(results => endpoints = results.data);
            
        return endpoints as Array<string>;
    }
}