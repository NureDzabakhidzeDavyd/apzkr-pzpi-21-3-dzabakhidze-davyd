import {Victim} from "./victim";
import {BaseEntity} from "./base-entity";
import {BrigadeRescuer} from "./brigade-rescuer";

export interface Contact extends BaseEntity {
  firstName: string;
  lastName: string;
  middleName: string;
  email: string;
  address: string;
  phone: string;
  dateOfBirth: Date;
  brigadeRescuer: BrigadeRescuer;
  victim: Victim;
  salt?: Uint8Array;
  password?: string;
  role: string;
  refreshToken?: string;
  refreshTokenExpiryTime?: Date;
}
