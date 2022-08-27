import axios from 'axios';

class PKCE {
    confirm(code: string) {
        const url = (process.env.VUE_APP_TOKEN_END_POINT).replace("{CODE_CHALLENGE}", code);
        const authorization = btoa(`${process.env.VUE_APP_CLIENT_ID}:${process.env.VUE_APP_CLIENT_SECRET}`)
        let results = null;
        const params = new URLSearchParams();
        params.append("grant_type", "authorization_code");
        params.append("code", code);
        params.append("redirect_uri", process.env.VUE_APP_REDIRECT_URI);

        axios.post(url
            , params
            , {
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                    "Authorization": `Basic ${authorization}`
                }
            }
        ).then(response => {
            results = response;
            localStorage.setItem("bearer-token", JSON.stringify(results))
            console.log(results);
        })
            .catch(() => { localStorage.setItem("bearer-token", "error") });
    }
}

export const security = new PKCE();