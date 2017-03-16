from django.db import models
from django.contrib.auth.models import User


class Save(models.Model):
    user = models.OneToOneField(User)

    created = models.DateTimeField(auto_now_add=True)
    attack = models.IntegerField()
    defence = models.IntegerField()
    speed = models.IntegerField()
    health = models.IntegerField()
    total_health = models.IntegerField()
    next_level = models.IntegerField()
    time = models.TimeField()

class Leaderboard(models.Model):
    user = models.ForeignKey(User)
    save = models.OneToOneField(Save)

class Item(models.Model):
    save_item = models.ManyToManyField(Save)

    item_id = models.IntegerField()