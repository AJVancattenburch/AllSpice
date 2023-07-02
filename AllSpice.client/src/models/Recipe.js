export class Recipe {
  constructor(data) {
    this.id = data.id;
    this.creator = data.creator;
    this.creatorId = data.creatorId;
    this.title = data.title;
    this.category = data.category;
    this.rating = data.rating;
    this.image = data.img;
    this.instructions = data.instructions;
    this.servings = data.servings;
    this.prepTime = data.prepTime;
    this.cookTime = data.cookTime;
    this.totalTime = data.totalTime;
    this.archived = data.archived;
    this.createdBy = data.creator.name;
    this.createdAt = data.createdAt;
    this.updatedAt = data.updatedAt;
  }
}