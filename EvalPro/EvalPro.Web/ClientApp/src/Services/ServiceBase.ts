import axios, {type AxiosInstance, type AxiosResponse} from "axios";

export interface ServiceOptions {
    handleResponseDates: boolean;
    baseURL?: string;
    endpoint: string;
}

const isoDateFormat = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2})(\.\d+)?(Z|([+-])(\d{2}):(\d{2}))$/;
const timeOnlyFormat = /^\d{2}:\d{2}:\d{2}(?:\.\d*)?$/;
const dateOnlyFormat = /^\d{4}-\d{2}-\d{2}$/;

function isIsoDateString(value: any): boolean {
    return value && typeof value === "string" && isoDateFormat.test(value);
}

function isDateOnlyString(value: any): boolean {
    return value && typeof value === "string" && dateOnlyFormat.test(value);
}

function isTimeOnlyString(value: any): boolean {
    return value && typeof value === "string" && timeOnlyFormat.test(value);
}

export class ServiceBase {
    private lazyApiInstance: LazyInstance<AxiosInstance>;

    constructor(init: (opts: ServiceOptions) => void) {
        const opts: ServiceOptions = {endpoint: "", handleResponseDates: true,};
        init(opts);
        const handleResponseDates = opts.handleResponseDates;
        this.lazyApiInstance = new LazyInstance<AxiosInstance>(() => {
            const result = axios.create({baseURL: "http://localhost:5285/api/" + opts.endpoint,});
            result.interceptors.response.use((resp) => {
                if (handleResponseDates && resp.data && resp.status >= 200 && resp.status < 300) {
                    this.handleDates(resp.data);
                }
                return resp;
            });
            return result;
        });
    }

    protected api(): AxiosInstance {
        return this.lazyApiInstance.value();
    }

    protected dateOnly(x: Date): Date {
        return new Date(Date.UTC(x.getFullYear(), x.getMonth(), x.getDate()));
    }

    protected ensureUtcDate(dateOrISOString: Date | string): Date {
        let x: Date;
        if (typeof dateOrISOString == "string") {
            x = new Date(dateOrISOString);
        } else {
            x = dateOrISOString;
        }
        return x;
    }

    private handleDates(body: any) {
        if (body === null || body === undefined || typeof body !== "object") {
            return body;
        }
        for (const key of Object.keys(body)) {
            const value = body[key];
            if (typeof value === "object") {
                this.handleDates(value);
            } else if (isIsoDateString(value)) {
                body[key] = this.ensureUtcDate(value);
            } else if (isDateOnlyString(value)) {
                body[key] = this.ensureUtcDate(value + "T00:00:00");
            } else if (isTimeOnlyString(value)) {
                body[key] = new Date("1970-01-01T" + value);
            }
        }
    }
}

export interface ServiceListArgs {
    path: string;
    query: { [key: string]: string | number };
}

export class ServiceCrudBase<TModel> extends ServiceBase {
    public get(id: any): Promise<TModel | null> {
        return super.api().get<TModel>(id.toString()).then((resp: AxiosResponse<TModel>) => {
            if (resp.status != 200) {
                return null;
            }
            return resp.data;
        });
    }

    public list(args?: Partial<ServiceListArgs>): Promise<TModel[]> {
        return super.api().get<TModel[]>(args?.path ?? "", {params: args?.query ?? {}}).then((resp: AxiosResponse<TModel[]>) => {
            return resp.data || [];
        });
    }

    public add(data: TModel): Promise<AxiosResponse> {
        return super.api().post<TModel>("", data);
    }

    public update(id: any, data: TModel): Promise<AxiosResponse> {
        return super.api().put<TModel>(id.toString(), data);
    }

    public delete(id: any): Promise<AxiosResponse> {
        return super.api().delete<TModel>(id.toString());
    }
}

class LazyInstance<T> {
    private readonly init: () => T;
    private instance: T | undefined;

    constructor(init: () => T) {
        this.init = init;
    }

    public value(): T {
        if (this.instance == undefined) {
            this.instance = this.init();
        }
        return this.instance;
    }
}