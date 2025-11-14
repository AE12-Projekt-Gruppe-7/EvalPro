import type {Kriterium} from "@/models/kriterium.ts";
import axios from "axios";

export class KriteriumServie {
    
    async getKriteriums(): Promise<void>{
        const instance = axios.create({});

        const result = await instance
            .get("/api/kriterium/")
            .then(result => result.data);
        console.log(result);
    }
}