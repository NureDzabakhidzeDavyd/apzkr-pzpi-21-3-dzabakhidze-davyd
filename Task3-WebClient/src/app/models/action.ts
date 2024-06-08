import {Victim} from "./victim";
import {BrigadeRescuer} from "./brigade-rescuer";
import {BaseEntity} from "./base-entity";

export interface Action extends BaseEntity {
  name: string;
  description: string;
  actionTime: string;
  actionType: string;
  actionPlace: string;
  brigadeRescuerId: string;
  brigadeRescuer: BrigadeRescuer;
  victimId: string;
  victim: Victim;
}
