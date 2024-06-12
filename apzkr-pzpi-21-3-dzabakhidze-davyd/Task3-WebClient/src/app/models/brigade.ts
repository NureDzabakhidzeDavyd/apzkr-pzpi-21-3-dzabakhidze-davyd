import {BrigadeRescuer} from "./brigade-rescuer";
import {BaseEntity} from "./base-entity";

export interface Brigade extends BaseEntity {
  name: string;
  description: string;
  brigadeSize: number;
  brigadeRescuers: BrigadeRescuer[];
}
