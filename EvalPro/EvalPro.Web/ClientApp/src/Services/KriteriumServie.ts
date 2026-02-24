import type {Kriterium} from "@/models/kriterium.ts";
import axios from "axios";
import { ServiceBase, type ServiceOptions } from "./ServiceBase.ts"

export default class KriteriumService extends ServiceBase {
    constructor() {         
        super((opts: ServiceOptions) => {
            opts.endpoint = "kriterium";        
        });     }
    
    async getKriteriums(): Promise<void>{
        const result = await super.api()
            .get("/")
            .then(result => result.data);
        console.log(result);
    }
}