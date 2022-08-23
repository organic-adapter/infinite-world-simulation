import { Hive } from "@/models/Hive";

export default {
    activeHives(state: any) {
        return state.activeHives;
    },
    focus(state: any) {
        return state.focus;
    },
    hiveByName: (state: any) => (hiveName: string) => {
        return state.activeHives.find((hive: Hive) => hive.name === hiveName) ?? null;
    }
}