import { security } from "@/services/security";
import { isEmpty } from "lodash";

class Authentication {
    async confirm(): Promise<boolean> {
        await this.resolveNewCode();
        return security.hasAuth();
    }
    getCodeMatches(): RegExpMatchArray | null {
        return window.location.search.match(/code=([^&]*)/);
    }
    getStateMatches(): RegExpMatchArray | null {
        return window.location.search.match(/state=([^&]*)/);
    }
    async resolveNewCode() {
        const codeIndex = 1;
        const stateIndex = 1;
        const codeMatches = this.getCodeMatches();
        const stateMatches = this.getStateMatches();
        if (
            codeMatches !== null
            && stateMatches !== null
            && codeMatches.length >= codeIndex
            && stateMatches.length >= stateIndex
        ) {
            const code = codeMatches[codeIndex];
            const state = stateMatches[stateIndex];
            await security.confirm(code, state);
        }
    }
    resolveToken(): Authentication {
        return this;
    }
}

export const auth = new Authentication();