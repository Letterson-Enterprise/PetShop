export interface Animal {
  id: number;
  nome: string;
  peso: number;
  dataNascimento: string;
  especie: string;
  tutorId: number;
}

export interface CreateAnimal {
  nome: string;
  peso: number;
  dataNascimento: string;
  especie: string;
  tutorId: number;
}

export type UpdateAnimal = CreateAnimal;
