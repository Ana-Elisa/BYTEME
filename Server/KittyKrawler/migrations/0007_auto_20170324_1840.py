# -*- coding: utf-8 -*-
# Generated by Django 1.10.5 on 2017-03-24 22:40
from __future__ import unicode_literals

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('KittyKrawler', '0006_auto_20170316_0341'),
    ]

    operations = [
        migrations.RemoveField(
            model_name='item',
            name='save_item',
        ),
        migrations.AddField(
            model_name='gamesave',
            name='save_item',
            field=models.ManyToManyField(to='KittyKrawler.Item'),
        ),
    ]