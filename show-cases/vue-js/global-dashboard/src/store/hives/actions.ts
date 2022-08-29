import { Hive } from "@/models/Hive";
import { services } from "@/services";
export default {
    getActiveHives: async (context: any) => {
        context.state.activeHives.splice(0);
        const hives = await services.getHiveEndpoints();
        hives.forEach((hive) => {
            context.state.activeHives.push(new Hive(hive.name, hive.baseEndpoint));
        });
    },
    setFocusByName: (context: any, hiveName: string) => {
        const hive = context.getters["hiveByName"](hiveName);
        if (hive === null || hive === undefined)
            context.commit("setFocus", new Hive(hiveName));
        else
            context.commit("setFocus", hive);
    },
    getWaterTick: async (context: any) => {
        const endpoint = context.getters["focus"].endpoint;        
        await services.getWaterTick(endpoint, "standard-tick-.json");
    }
}