import { Profile } from "./Account.js"
import { Recipe } from "./Recipe.js"

export class Favorite {
  constructor(data) {
    this.id = data.id
    this.recipeId = data.recipeId
    this.accountId = data.accountId
  }
}

// export class MyFavorites extends Recipe {
//   constructor(data) {
//     super(data)
//     this.favoriteId = data.favoriteId
//   }
// }

// export class FavoritesProfile extends Profile {
//   constructor(data) {
//     super(data)
//     this.favoriteId = data.favoriteId
//   }
// }