export class Hive {
    constructor(name = "", endpoint = "") {
        this.name = name;
        this.endpoint = endpoint;
    }
    name: string
    endpoint: string
}