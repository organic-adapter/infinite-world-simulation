import { Hive } from "@/models/Hive";
import { cloneDeep } from "lodash";

export default {
    setFocus(state: any, value: Hive) {
        state.focus = cloneDeep(value);
    }
}