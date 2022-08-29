import axios from 'axios';

declare global {
    interface Crypto {
      randomUUID: () => string;
    }
  }

const KEYS = {
    SECURITY_NONCE: "sec_nonce",
    OIDC_RETURN: "oidc",
}

class PKCE {
    attachSecurity(config: any) {
        const oidcJson = localStorage.getItem(KEYS.OIDC_RETURN) || "{}";
        const oidc = JSON.parse(oidcJson);

        config.headers = config.headers || {};
        config.headers["Authorization"] = `${oidc.token_type} ${oidc.id_token}`;
    }
    async confirm(code: string, state: string) {
        if (state !== localStorage.getItem(KEYS.SECURITY_NONCE))
        {
            console.log("State nonce did not match. Possible XSFR issue or race condition.")
            return false;
        }
        const url = (process.env.VUE_APP_TOKEN_END_POINT).replace("{CODE_CHALLENGE}", code);
        const authorization = btoa(`${process.env.VUE_APP_CLIENT_ID}:${process.env.VUE_APP_CLIENT_SECRET}`)
        const params = new URLSearchParams();
        params.append("grant_type", "authorization_code");
        params.append("code", code);
        params.append("redirect_uri", process.env.VUE_APP_REDIRECT_URI);

        await axios.post(url
                , params
                , {
                    headers: {
                        "Content-Type": "application/x-www-form-urlencoded",
                        "Authorization": `Basic ${authorization}`
                    }
                }
            )
            .then(response => {
                localStorage.setItem(KEYS.OIDC_RETURN, JSON.stringify(response.data))
                localStorage.removeItem(KEYS.SECURITY_NONCE);
                return true;
            })
            .catch(() => {
                localStorage.removeItem(KEYS.OIDC_RETURN);
                localStorage.removeItem(KEYS.SECURITY_NONCE);
                return false;
            });
    }
    hasAuth() {
        const token = localStorage.getItem(KEYS.OIDC_RETURN);
        return token !== null;
    }
    async checkAuth() {
        const token = localStorage.getItem(KEYS.OIDC_RETURN);
        if (token === null) {
            const nonce = crypto.randomUUID();
            const login = process.env?.VUE_APP_IMP_AUTHORIZATION_END_POINT.replace("{NONCE}", nonce);
            localStorage.setItem(KEYS.SECURITY_NONCE, nonce);
            window.open(login, '_self');
        }
    }
}

export const security = new PKCE();