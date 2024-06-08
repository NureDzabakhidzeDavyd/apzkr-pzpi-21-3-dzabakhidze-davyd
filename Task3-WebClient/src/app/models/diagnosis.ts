import {Victim} from "./victim";
import {BaseEntity} from "./base-entity";

export interface Diagnosis extends BaseEntity {
  name: string;
  note: string;
  detectionTime: Date;
  victimId: string;
  victim: Victim;
}
