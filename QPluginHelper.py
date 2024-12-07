import os
import json
import time
import xml.etree.ElementTree as xmlTree

from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler

# Путь к директории с плагинами
PLUGINS_DIR = './plugins'
# Путь к файлу, где будет храниться список плагинов
PLUG_LIST_FILE = './plugList.json'
# Путь к XML файлу
MISK_XML_FILE = './.idea/modules.xml'


def update_index():
    """Функция для обновления XML файла на основе списка плагинов."""
    # Читаем список плагинов из plugList.json
    with open(PLUG_LIST_FILE, 'r') as f:
        plugin_list = json.load(f)["plugin_list"]

    # Парсим XML файл
    if not os.path.exists(MISK_XML_FILE):
        with open(MISK_XML_FILE, 'w') as f:
            f.write('''
             <project version="4">
                <component name="ProjectModuleManager">
                    <modules>
                    </modules>
                </component>
            </project>
            ''')
    tree = xmlTree.parse(MISK_XML_FILE)
    root = tree.getroot()

    # Удаляем старые записи о плагинах
    for element in root.findall('module'):
        attrib = element.attrib["filepath"]
        if attrib.replace("$PROJECT_DIR$/", "") in plugin_list:
            root.remove(element)

    # Добавляем новые записи о плагинах
    for plugin in plugin_list:
        iml_file = plugin['path']
        if os.path.exists(iml_file):
            module_element = xmlTree.SubElement(root, 'module', attrib={
                'fileurl': f'file://{iml_file}',
                'filepath': iml_file
            })
            root.append(module_element)

    # Сохраняем обновленный XML файл
    tree.write(MISK_XML_FILE, encoding='utf-8', xml_declaration=True)
    print(f"XML file updated: {MISK_XML_FILE}")


def save_plugin_list():
    """Сохраняет список папок в директории PLUGINS_DIR в файл PLUG_LIST_FILE."""
    plugin_list = []
    for name in os.listdir(PLUGINS_DIR):
        plugin_dir = os.path.join(PLUGINS_DIR, name)
        if os.path.isdir(plugin_dir):
            iml_file = os.path.join(plugin_dir, f"{name}.iml")
            if os.path.exists(iml_file):
                plugin_list.append(name)

    with open(PLUG_LIST_FILE, 'w') as f:
        data = {
            "plugin_list": plugin_list
        }
        json.dump(data, f)
    print(f"Plugin list saved to {PLUG_LIST_FILE}")


class PluginHandler(FileSystemEventHandler):
    """Обработчик событий для отслеживания изменений в директории PLUGINS_DIR."""

    def on_created(self, event):
        """Вызывается при создании нового файла или директории."""
        if event.is_directory:
            print(f"New plugin directory created: {event.src_path}")
            save_plugin_list()
            update_index()

    def on_deleted(self, event):
        """Вызывается при удалении файла или директории."""
        if event.is_directory:
            print(f"Plugin directory deleted: {event.src_path}")
            save_plugin_list()
            update_index()


def main():
    # Создаем наблюдателя и регистрируем обработчик событий
    event_handler = PluginHandler()
    observer = Observer()
    observer.schedule(event_handler, path=PLUGINS_DIR, recursive=False)

    # Запускаем наблюдателя
    observer.start()

    try:
        # Сохраняем текущий список плагинов при старте
        save_plugin_list()
        update_index()  # Вызов абстрактной функции x при старте

        # Бесконечный цикл для поддержания работы наблюдателя
        while True:
            time.sleep(0.5)
    except KeyboardInterrupt:
        # Останавливаем наблюдателя при завершении программы
        observer.stop()
    observer.join()
    print("Index updating stopped")


if __name__ == "__main__":
    main()
