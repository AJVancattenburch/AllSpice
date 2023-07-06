function formatDateAndTime(dateString) {
  const date = new Date(dateString);
  // Format date as MM/DD/YYYY
  const formattedDate = `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()}`;
  // Format time as regular 12-hour format
  let hours = date.getHours();
  const minutes = date.getMinutes();
  const amPm = hours >= 12 ? 'PM' : 'AM';
  hours = hours % 12 || 12;
  const formattedTime = `${hours}:${minutes.toString().padStart(2, '0')} ${amPm}`;
  return {formattedDate, formattedTime};
}

export class Ingredient {
  constructor(data) {
    this.id = data.id;
    this.name = data.name || '';
    this.quantity = data.quantity || 0;
    this.recipeId = data.recipeId;
    this.recipe = data.recipe;
    this.createdAt = formatDateAndTime(data.createdAt).formattedDate + ' ' + formatDateAndTime(data.createdAt).formattedTime;
    this.updatedAt = formatDateAndTime(data.updatedAt).formattedDate + ' ' + formatDateAndTime(data.updatedAt).formattedTime;
  }
}