import { Hive } from "@/models/Hive";

export default {
    getActiveHives: (context: any) => {
        context.state.activeHives.splice(0);
        context.state.activeHives.push(new Hive("Mock-001"));
        context.state.activeHives.push(new Hive("Mock-002"));
    },
    setFocusByName: (context: any, hiveName: string) => {
        const hive = context.getters["hiveByName"](hiveName);
        if (hive === null || hive === undefined)
            context.commit("setFocus", new Hive(hiveName));
        else
            context.commit("setFocus", hive);
    },
}