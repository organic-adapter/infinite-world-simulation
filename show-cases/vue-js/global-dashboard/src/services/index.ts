import axios from 'axios';
import { security } from './security';

class Services {
    async getHiveEndpoints(): Promise<any[]> {
        const endpoints: Array<any> = [];
        await axios.get(`${process.env.VUE_APP_HIVE_END_POINT_API}/endpoint`)
            .then(results => endpoints.push(...results.data));

        return endpoints as Array<any>;
    }
    async getWaterTick(hiveEndPoint: string, tickId: string): Promise<any> {
        const config = {};
        security.attachSecurity(config);
        return await axios.get(`${hiveEndPoint}/water/api/watertick/${tickId}`, config);
    }
}

export const services = new Services();