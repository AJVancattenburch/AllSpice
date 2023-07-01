export class Ingredient {
  constructor(data) {
    this.id = data.id;
    this.name = data.name;
    this.quantity = data.quantity;
    this.recipeId = data.recipeId;
    this.createdAt = data.createdAt;
    this.updatedAt = data.updatedAt;
  }
}