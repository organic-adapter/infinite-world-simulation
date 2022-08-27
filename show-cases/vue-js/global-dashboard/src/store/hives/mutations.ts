import { Hive } from "@/models/Hive";
import { cloneDeep } from "lodash";

export default {
    setActiveHives(state: any, value: Array<string>) {
        state.activeHives.splice(0);
        state.activeHives.push(...value);
    },
    setFocus(state: any, value: Hive) {
        state.focus = cloneDeep(value);
    }
}