import { Animal } from './animal.model';
import { Endereco } from './endereco.model';

export interface Tutor {
  id: number;
  nome: string;
  dataNascimento: string;
  endereco: Endereco;
  animais: Animal[];
}

export interface CreateTutor {
  nome: string;
  dataNascimento: string;
  cep: string;
  numeroCasa: number;
}

export type UpdateTutor = CreateTutor;
