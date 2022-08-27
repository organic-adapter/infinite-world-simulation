import { security } from "@/assets/security";
import { isEmpty } from "lodash";

class Authentication {
    confirm(): boolean {
        this.resolveNewCode()
            .resolveToken();

        return false;
    }
    getCodeMatches(): RegExpMatchArray | null {
        return window.location.search.match(/code=([^&]*)/);
    }
    resolveNewCode(): Authentication {
        const codeIndex = 1;
        const codeMatches = this.getCodeMatches();
        if (codeMatches !== null && codeMatches.length >= codeIndex) {
            const code = codeMatches[codeIndex];
            security.confirm(code);
        }
        return this;
    }
    resolveToken(): Authentication {
        return this;
    }
}

export const auth = new Authentication();