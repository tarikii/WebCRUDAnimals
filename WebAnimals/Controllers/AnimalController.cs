using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAnimals.Models;

namespace WebAnimals.Controllers
{
    public class AnimalController : Controller
    {
        Random random = new Random();
        AnimalRepository _animalRepository = new AnimalRepository();

        [HttpGet]
        public ActionResult ListaAnimales()
        {
            List<Animal> animals = _animalRepository.GetAllAnimalsDB();

            int animalRandom = random.Next(0, animals.Count);

            ViewBag.AnimalSuerte = "Tu animal de la suerte es: " + animals[animalRandom].NombreAnimal;

            return View(animals);
        }

        [HttpGet]
        public ActionResult CrearAnimal()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CrearAnimal(Animal animal)
        {
            if (ModelState.IsValid)
            {
                bool isCreated = _animalRepository.CreateAnimalDB(animal);

                if (isCreated)
                    return RedirectToAction("ListaAnimales");
                else
                    ViewBag.Error = "Unable to create animal. Please try again.";

            }
            return View(animal);
        }

        [HttpGet]
        public ActionResult ActualizarAnimal()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ActualizarAnimal(Animal animal)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = _animalRepository.UpdateAnimalDB(animal);

                if (isUpdated)
                    return RedirectToAction("ListaAnimales");
                else
                    ViewBag.Error = "Unable to update animal. Please try again.";
            }

            return View(animal);
        }

        [HttpGet]
        public ActionResult DeleteAnimal()
        {
            return View();
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAnimal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Animal animal = _animalRepository.GetAnimalDB(id.Value);
            if (animal != null)
            {
                bool isDeleted = _animalRepository.DeleteAnimalDB(id.Value);

                if (isDeleted)
                    return RedirectToAction("ListaAnimales");
                else
                    ViewBag.Error = "Unable to delete the animal. Check the IDs";
            }
            else
            {
                ViewBag.Error = "Animal not found";
            }

            return View();
        }
    }
}